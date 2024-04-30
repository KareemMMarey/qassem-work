namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestComparer : IEqualityComparer<UploadRequestDto>
    {
        public bool Equals(UploadRequestDto x1, UploadRequestDto x2)
        {
            if (ReferenceEquals(x1, x2))
                return true;
            if (x1 == null || x2 == null)
                return false;
            return x1.Id.Equals(x2.Id);
        }

        public int GetHashCode(UploadRequestDto x)
        {
            return x.Id.GetHashCode();
        }
    }
}