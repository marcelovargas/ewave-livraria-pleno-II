namespace LivreriaWeb.Helpers
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Clase de ajuda de arquivos.
    /// </summary>
    public class FilesHelper
    {
        /// <summary>
        /// Ajuda a subir uma arquivo ao servidor.
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        internal static async Task<string> UploadPhoto(IFormFile imageFile, string capa)
        {
            var path = string.Empty;

            if (imageFile != null && imageFile.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                var path1 = Path.Combine(
                   Directory.GetCurrentDirectory(),
                   "wwwroot\\imagens\\livros",
                   file);

                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                path = $"~/imagens/livros/{file}";
            }
            else
            {
                path = capa;
            }

            return path;
        }
    }
}
