﻿using Raven.Abstractions.Logging;
using Raven.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavendbSample1
{
    public abstract class BaseRepository<TObject>
     : ForaDocumentStore,
         /// IRepository<TObject>,
         IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseRepository<TObject>));

        private IDocumentSession session;
        public IDocumentSession Session
        {
            get
            {
                if (session == null)
                {
                    session = Store.OpenSession();
                }
                return session;
            }
        }

        public virtual IQueryable GetDocuments()
        {
            return Session.Query<TObject>();
        }

        public virtual TObject GetDocument(int id)
        {
            return Session.Load<TObject>(id);
        }

        public virtual IQueryable GetDocuments(IEnumerable<string> ids)
        {
            return Session.Load<TObject>(ids).AsQueryable();
        }

        public virtual void AddDocument(TObject entity)
        {
            try
            {
                Session.Store(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error("Exception message : {0}", ex);
            }

        }

        public virtual void UpdateDocument(TObject entity)
        {
            try
            {
                Session.Store(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error("Exception message : {0}", ex);
            }
        }

        public virtual void DeleteDocument(TObject entity)
        {
            try
            {
                Session.Delete(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error("Exception message : {0}", ex);
            }
        }

        public virtual void DeleteDocument(int id)
        {
            try
            {
                TObject entity = GetDocument(id);
                Session.Delete(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error("Exception message : {0}", ex);
            }
        }

        public void Dispose()
        {
            Session.SaveChanges();
            Session.Dispose();
        }

        public void SaveChanges()
        {
            Session.SaveChanges();
        }
    }
}
