using System;
using System.Collections.Generic;
using System.Text;

using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic.relation;
using umbraco.cms.businesslogic.property;
using umbraco.cms.businesslogic;

namespace Goyaweb.MultiLanguage
{

    // code by Tim Geyssens
    public class CopyDocumentToRelatedOnPublish : ApplicationBase
    {
        //Constructor
        public CopyDocumentToRelatedOnPublish()
        {
            //subscribe to the afterpublish events
            //Document.AfterPublish += new Document.PublishEventHandler(Document_AfterPublish);
        }

        /// <summary>
        /// Executes after document publishing
        /// </summary>
        /// <param name="sender">The sender (a document object).</param>
        /// <param name="e">The <see cref="umbraco.cms.businesslogic.PublishEventArgs"/> instance containing the event data.</param>
        void Document_AfterPublish(Document sender, umbraco.cms.businesslogic.PublishEventArgs e)
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
