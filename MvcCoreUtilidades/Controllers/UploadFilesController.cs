using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile file)
        {
            ////VAMOS A SUBIR A LOS ELEMENTOS TEMPORALES 
            //string tempFolder = Path.GetTempPath();
            //string fileName = file.FileName;

            ////Cuando pensamos en ficheros y en sus rutas 
            ////Pensamos en algo parecido a esto
            ////C:\Users\Usuario\Desktop\file.txt
            ////Net core no es windows y las de linux pueden ser distintas
            ////Debemos crear rutas siempre con herramientas de netcore para que sean compatibles con cualquier sistema operativo
            //string path = Path.Combine(tempFolder, fileName);
            ////PARA SUBIR FICHEROS SE UTILIZA STRING
            //using (Stream stream = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}
            //ViewData["MENSAJE"] = "fichero subido a : " + path;
            //return View();


            string rootFolder = this.hostEnvironment.WebRootPath;
            string fileName = file.FileName;

            //Cuando pensamos en ficheros y en sus rutas 
            //Pensamos en algo parecido a esto
            //C:\Users\Usuario\Desktop\file.txt
            //Net core no es windows y las de linux pueden ser distintas
            //Debemos crear rutas siempre con herramientas de netcore para que sean compatibles con cualquier sistema operativo
            string path = Path.Combine(rootFolder,"uploads",fileName);
            //PARA SUBIR FICHEROS SE UTILIZA STRING
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "fichero subido a : " + path;
            ViewData["FILENAME"] = fileName;


            return View();
        }
    }
}
