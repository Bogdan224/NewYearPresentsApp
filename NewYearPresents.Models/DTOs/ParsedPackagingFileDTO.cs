using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.DTOs
{
    public class ParsedPackagingFileDTO(List<Packaging>? packagings, List<PackagingInStorage>? packagingsInStorages)
    {
        public List<Packaging>? Packagings { get; } = packagings;
        public List<PackagingInStorage>? PackagingsInStorage { get; } = packagingsInStorages;
    }
}
