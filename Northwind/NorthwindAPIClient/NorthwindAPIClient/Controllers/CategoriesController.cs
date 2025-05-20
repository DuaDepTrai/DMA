using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindAPIClient.Models;

namespace NorthwindAPIClient.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MySettings mst = new MySettings();
        public CategoriesController(IOptions<MySettings> options)
        {
            mst = options.Value;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            RestClient restClient = new RestClient();
            //restClient.BaseUrl = "https://localhost:44374/";
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories";
            string result = restClient.RestRequestAll();

            List<Categories> CatesList = JsonConvert.DeserializeObject<List<Categories>>(result);

            return View(CatesList);
        }

        public ActionResult ShowImage(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);
            if (cate == null || cate.Picture == null)
                return null;

            byte[] imageData = cate.Picture;
            string mimeType = "image/jpeg"; // Mặc định

            // Kiểm tra header để xác định định dạng
            if (imageData.Length >= 78) // Đảm bảo đủ byte để kiểm tra
            {
                // PNG: Bắt đầu bằng 89504E47
                if (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
                {
                    mimeType = "image/png";
                    // Giữ nguyên toàn bộ dữ liệu cho PNG
                }
                // BMP: Kiểm tra chuỗi 'Bitmap Image' tại offset 20 hoặc chữ ký BMP '42 4D' tại offset 76
                else if (imageData.Skip(20).Take(12).SequenceEqual(Encoding.ASCII.GetBytes("Bitmap Image")) ||
                         (imageData.Length >= 78 && imageData[76] == 0x42 && imageData[77] == 0x4D))
                {
                    mimeType = "image/bmp";
                    imageData = cate.Picture is null ? null : cate.Picture.Skip(78).ToArray();
                    if (cate != null && cate.Picture != null)
                    {
                        return File(imageData, mimeType);
                    }
                    return null;
                }
                // JPG/JPEG: Bắt đầu bằng FFD8FF
                else if (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
                {
                    mimeType = "image/jpeg";
                    // Giữ nguyên toàn bộ dữ liệu cho JPG/JPEG
                }
                // GIF: Bắt đầu bằng 47494638 (GIF87a hoặc GIF89a)
                else if (imageData[0] == 0x47 && imageData[1] == 0x49 && imageData[2] == 0x46 && imageData[3] == 0x38)
                {
                    mimeType = "image/gif";
                    // Giữ nguyên toàn bộ dữ liệu cho GIF
                }
                else
                {
                    // Định dạng không hỗ trợ
                    return null;
                }
            }
            else
            {
                // Không đủ byte để kiểm tra
                return null;
            }

            return File(imageData, mimeType);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories obj, string PictureBase64)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Chuyển Base64 thành byte[] nếu có ảnh và kiểm tra định dạng
                    if (!string.IsNullOrEmpty(PictureBase64))
                    {
                        byte[] imageData = Convert.FromBase64String(PictureBase64);

                        // Kiểm tra định dạng ảnh
                        if (imageData.Length >= 78) // Đảm bảo đủ byte để kiểm tra
                        {
                            // PNG: 89504E47
                            if (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
                            {
                                obj.Picture = imageData; // Lưu toàn bộ dữ liệu PNG
                            }
                            // BMP: Kiểm tra 'Bitmap Image' tại offset 20 hoặc '42 4D' tại offset 76
                            else if (imageData.Skip(20).Take(12).SequenceEqual(Encoding.ASCII.GetBytes("Bitmap Image")) ||
                                     (imageData.Length >= 78 && imageData[76] == 0x42 && imageData[77] == 0x4D))
                            {
                                obj.Picture = imageData; // Lưu toàn bộ dữ liệu BMP (bao gồm 78 byte header)
                            }
                            // JPG/JPEG: FFD8FF
                            else if (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
                            {
                                obj.Picture = imageData; // Lưu toàn bộ dữ liệu JPG/JPEG
                            }
                            // GIF: 47494638
                            else if (imageData[0] == 0x47 && imageData[1] == 0x49 && imageData[2] == 0x46 && imageData[3] == 0x38)
                            {
                                obj.Picture = imageData; // Lưu toàn bộ dữ liệu GIF
                            }
                            else
                            {
                                ModelState.AddModelError("", "Image formats are not supported. Only supports BMP, PNG, JPG, JPEG, GIF.");
                                return View(obj);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Image data is invalid or too small.");
                            return View(obj);
                        }
                    }

                    RestClient restClient = new RestClient();
                    restClient.BaseUrl = mst.BaseUrl;
                    restClient.endPoint = "api/Categories";
                    string result = restClient.RestPostObj(obj);
                    if (result == "Success")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create category.");
                        return View(obj);
                    }
                }
                return View(obj);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Error: {e.Message}");
                return View(obj);
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories obj, string PictureBase64)
        {
            try
            {
                // Log ModelState để debug
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
                }

                // Lấy dữ liệu category hiện tại từ API để giữ ảnh cũ nếu không upload ảnh mới
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories/" + obj.CategoryID;
                string currentResult = restClient.RestRequestAll();
                if (string.IsNullOrEmpty(currentResult))
                {
                    ModelState.AddModelError("", "API returned empty response for current category.");
                    return View(obj);
                }

                Categories currentCate = JsonConvert.DeserializeObject<Categories>(currentResult);
                if (currentCate == null)
                {
                    ModelState.AddModelError("", "Cannot retrieve current category data from API.");
                    return View(obj);
                }

                // Chuyển Base64 thành byte[] nếu có ảnh mới và kiểm tra định dạng
                if (!string.IsNullOrEmpty(PictureBase64))
                {
                    byte[] imageData;
                    try
                    {
                        imageData = Convert.FromBase64String(PictureBase64);
                    }
                    catch (FormatException)
                    {
                        ModelState.AddModelError("", "Invalid Base64 image data.");
                        return View(obj);
                    }

                    // Kiểm tra định dạng ảnh
                    if (imageData.Length >= 78) // Đảm bảo đủ byte để kiểm tra
                    {
                        // PNG: 89504E47
                        if (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
                        {
                            obj.Picture = imageData; // Lưu dữ liệu PNG
                        }
                        // BMP: Kiểm tra 'Bitmap Image' tại offset 20 hoặc '42 4D' tại offset 76
                        else if (imageData.Skip(20).Take(12).SequenceEqual(Encoding.ASCII.GetBytes("Bitmap Image")) ||
                                 (imageData.Length >= 78 && imageData[76] == 0x42 && imageData[77] == 0x4D))
                        {
                            obj.Picture = imageData; // Lưu dữ liệu BMP
                        }
                        // JPG/JPEG: FFD8FF
                        else if (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
                        {
                            obj.Picture = imageData; // Lưu dữ liệu JPG/JPEG
                        }
                        // GIF: 47494638
                        else if (imageData[0] == 0x47 && imageData[1] == 0x49 && imageData[2] == 0x46 && imageData[3] == 0x38)
                        {
                            obj.Picture = imageData; // Lưu dữ liệu GIF
                        }
                        else
                        {
                            ModelState.AddModelError("", "Image formats are not supported. Only supports BMP, PNG, JPG, JPEG, GIF.");
                            return View(obj);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Image data is invalid or too small.");
                        return View(obj);
                    }
                }
                else
                {
                    // Giữ nguyên ảnh cũ nếu không upload ảnh mới
                    obj.Picture = currentCate.Picture ?? obj.Picture; // Fallback to obj.Picture if currentCate.Picture is null
                }

                // Gửi yêu cầu cập nhật
                restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories";
                string result;
                try
                {
                    result = restClient.RestPutObj(obj);
                    Console.WriteLine($"RestPutObj result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RestPutObj exception: {ex.Message}");
                    ModelState.AddModelError("", $"API update failed: {ex.Message}");
                    return View(obj);
                }

                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", $"Failed to update category. API response: {result}");
                    return View(obj);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Error: {e.Message}");
                return View(obj);
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestClient restClient = new RestClient();
            restClient.BaseUrl = mst.BaseUrl;
            restClient.endPoint = "api/Categories/" + id;
            string result = restClient.RestRequestAll();

            Categories cate = JsonConvert.DeserializeObject<Categories>(result);

            return View(cate);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = mst.BaseUrl;
                restClient.endPoint = "api/Categories/" + id;
                string result = restClient.RestDeleteObj();
                if (result == "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
