using ETicaretAPI.Infrastructure.Operations;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, int num = 1)
        {
            return await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = $"{Path.GetFileNameWithoutExtension(fileName)}-{num}";
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                //if (File.Exists($"{pathOrContainerName}\\{newFileName}"))
                if (hasFileMethod(pathOrContainerName, newFileName))
                {
                    return await FileRenameAsync(pathOrContainerName, $"{newFileName.Split($"-{num}")[0]}{extension}", hasFileMethod,++num);
                }
                return newFileName;
            });
        }
    }
}
