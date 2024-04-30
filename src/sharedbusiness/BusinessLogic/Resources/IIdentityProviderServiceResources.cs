


using BusinessLogic.Helpers;

namespace BusinessLogic.Resources
{
    public interface IIdentityProviderServiceResources
    {
        ResourceMessage IdentityProviderDoesNotExist();

        ResourceMessage IdentityProviderExistsKey();

        ResourceMessage IdentityProviderExistsValue();

    }
}
