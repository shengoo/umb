using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.relation;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;

namespace Site.EventHandlers
{
    public class CreateRelatedDocumentAfterPublish : IApplicationEventHandler
    {

        private static object _lockObj = new object();
        private static bool _ran = false; 


        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // lock
            if (!_ran)
            {
                lock (_lockObj)
                {
                    if (!_ran)
                    {
                        // everything we do here is blocking
                        // on application start, so we should be 
                        // quick. 
                        // do you're registering here... or in a function 
                        // you will need to add the relevent class at the top of your code (i.e using Umbraco.cms.businesslogic.web)
                        
                        Document.AfterPublish += new Document.PublishEventHandler(Document_AfterPublish);
                        _ran = true;
                    }
                }
            }
        }

        void Document_AfterPublish(Document sender, PublishEventArgs e)
        {

            // if the parent doesn't already have been related
            bool hasCopy = false;
            if (sender.Relations.Length > 0 && sender.Level > 1)
            {
                foreach (Relation r in sender.Parent.Relations)
                {
                    if (r.RelType.Alias == "relateDocumentOnCopy")
                    {
                        hasCopy = true;
                        break;
                    }
                }
            }

            // if the parent documentObject have a relation
            if (!hasCopy && sender.Level > 1)
            {
                Document parent = new Document(sender.Parent.Id);
                if (parent.Relations.Length > 0)
                {
                    foreach (Relation r in parent.Relations)
                    {
                        if (r.RelType.Alias == "relateDocumentOnCopy")
                        {
                            sender.Copy(r.Child.Id, sender.User, true);
                        }
                    }
                }
            }
        }

    }
}