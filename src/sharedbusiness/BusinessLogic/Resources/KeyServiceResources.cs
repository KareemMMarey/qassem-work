


using BusinessLogic.Helpers;

namespace BusinessLogic.Resources
{
    public class KeyServiceResources : IKeyServiceResources
    {
        public ResourceMessage KeyDoesNotExist()
        {
            return new ResourceMessage()
            {
                Code = nameof(KeyDoesNotExist),
                Description = KeyServiceResource.KeyDoesNotExist
            };
        }
    }
}