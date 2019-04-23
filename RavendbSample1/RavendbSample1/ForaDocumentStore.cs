using Raven.Client;
using Raven.Client.Document;
using System;

namespace RavendbSample1
{
    public class ForaDocumentStore
    {
        public static IDocumentStore Store
        {
            get { return LazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new DocumentStore();
            docStore.Url = EnvironmentConstants.DbServerUrl;
            docStore.DefaultDatabase = EnvironmentConstants.DbName;

            docStore.Initialize();
            return docStore;
        });
    }
}