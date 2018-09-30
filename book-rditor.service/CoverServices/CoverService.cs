using AutoMapper;
using book_editor.data.DB.Models;
using book_editor.service.Mapper;
using book_editor.service.Utility;
using book_editor.service.ViewModels;
using System;
using System.IO;
using System.Web;

namespace book_editor.service.CoverServices
{
    public class CoverService : ICoverService
    {
        #region ctor


        IRepository<Cover> _repository;
        IMapper _mapper;
        public CoverService(IRepository<Cover> repository, IMapperConfigurator mapperConfigurator)
        {
            _repository = repository;
            _mapper = mapperConfigurator.GetMapper();
        }

        #endregion

        public int Save(int bookId, HttpPostedFileBase cover)
        {
            var file = cover;
            if (file != null && file.ContentLength > 0)
            {
                byte[] content;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    content = br.ReadBytes(file.ContentLength);
                }

                var coverEntity = _repository.Find(x => x.BookId == bookId);
                Action<Cover> save = cov => _repository.Update(coverEntity);
                if (coverEntity == null)
                {
                    coverEntity = new Cover();
                    save= cov => _repository.Create(coverEntity);
                }

                coverEntity.BookId = bookId;
                coverEntity.FileName = file.FileName;
                coverEntity.File = content;
                save(coverEntity);
                _repository.Save();
            }
            return bookId;
        }

        public CoverViewModel Get(int bookId)
        {
            var coverEntity = _repository.Find(x => x.BookId == bookId);
            if (coverEntity == null)
            {
                return null;
            }
            var coverViewModel = _mapper.Map<CoverViewModel>(coverEntity);

            return coverViewModel;
        }

        public void Delete(int bookId)
        {
            var coverEntity = _repository.Find(x => x.BookId == bookId);
            if (coverEntity != null)
            {
                _repository.HardDelete(coverEntity.Id);
                _repository.Save();
            }

        }
    }
}
