using System;
using System.Collections.Generic;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;

namespace DLCS.Client.Model
{
    public class CommonOperations
    {
        // TODO - in general, add statusCode hints everywhere


        public static Operation[] GetStandardResourceOperations(
            string idPrefix,
            string displayNameOfCollectionType,
            string vocabNameofCollectionType,
            params string[] methods)
        {
            var ops = new List<Operation>();
            foreach (var method in methods)
            {
                switch (method)
                {
                    case "GET":
                        ops.Add(new Operation
                        {
                            Id = idPrefix + "_retrieve",
                            Method = method,
                            Label = "Retrieve a " + displayNameOfCollectionType,
                            Returns = vocabNameofCollectionType,
                            StatusCodes = GetStandardGetResourceStatusCodes(displayNameOfCollectionType)
                        });
                        break;

                    case "PUT":
                        ops.Add(new Operation
                        {
                            Id = idPrefix + "_upsert",
                            Method = method,
                            Label = "create or replace a " + displayNameOfCollectionType,
                            Expects = vocabNameofCollectionType,
                            Returns = vocabNameofCollectionType,
                            StatusCodes = GetStandardPutResourceStatusCodes(displayNameOfCollectionType)
                        });
                        break;

                    case "PATCH":
                        ops.Add(new Operation
                        {
                            Id = idPrefix + "_update",
                            Method = method,
                            Label = "Update the supplied fields of the " + displayNameOfCollectionType,
                            Expects = vocabNameofCollectionType,
                            Returns = vocabNameofCollectionType,
                            StatusCodes = GetStandardPatchResourceStatusCodes(displayNameOfCollectionType)
                        });
                        break;

                    case "DELETE":
                        ops.Add(new Operation
                        {
                            Id = idPrefix + "_delete",
                            Method = method,
                            Label = "Delete the " + displayNameOfCollectionType,
                            Expects = null,
                            Returns = Names.Owl.Nothing,
                            StatusCodes = GetStandardDeleteResourceStatusCodes(displayNameOfCollectionType)
                        });
                        break;

                    default:
                        throw new ArgumentException("Unknown HTTP method " + method);

                }
            }
            return ops.ToArray();

        }


        public static Operation[] GetStandardCollectionOperations(
            string idPrefix, 
            string displayNameOfCollectionType,
            string vocabNameofCollectionType)
        {
            return new[]
            {
                StandardCollectionGet(idPrefix + "_collection_retrieve", "Retrieves all " + displayNameOfCollectionType, null),
                StandardCollectionPost(idPrefix + "_create", "Creates a new " + displayNameOfCollectionType, 
                    null, vocabNameofCollectionType, displayNameOfCollectionType)
            };

        }
        public static Operation StandardCollectionGet(string id, string label, string description)
        {
            return new Operation
            {
                Id = id,
                Method = "GET",
                Label = label,
                Description = description,
                Returns = Names.Hydra.Collection
            };
        }

        public static Operation StandardCollectionPost(string id, string label, string description, 
            string expectsAndReturns, string displayNameOfCollectionType)
        {
            return new Operation
            {
                Id = id,
                Method = "POST",
                Label = label,
                Description = description,
                Expects = expectsAndReturns,
                Returns = expectsAndReturns,
                StatusCodes = GetStandardPostToCollectionStatusCodes(displayNameOfCollectionType)
            };
        }
        
        public static Status[] GetStandardPostToCollectionStatusCodes(string resourceName)
        {
            return new[]
            {
                new Status
                {
                    StatusCode = 201,
                    Description = resourceName + " created."
                }
            };
        }

        public static Status[] GetStandardPatchResourceStatusCodes(string resourceName)
        {
            return new[]
            {
                new Status
                {
                    StatusCode = 200,
                    Description = "patched " + resourceName
                }
            };
        }


        private static Status[] GetStandardDeleteResourceStatusCodes(string displayNameOfCollectionType)
        {
            // TODO - GetStandardDeleteResourceStatusCodes
            return null;
        }
        private static Status[] GetStandardGetResourceStatusCodes(string displayNameOfCollectionType)
        {
            // TODO - GetStandardGetResourceStatusCodes
            return null;
        }
        private static Status[] GetStandardPutResourceStatusCodes(string displayNameOfCollectionType)
        {
            // TODO - GetStandardPutResourceStatusCodes
            return null;
        }
    }
}
