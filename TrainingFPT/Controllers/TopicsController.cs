using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPT.Helpers;
using TrainingFPT.Models;
using TrainingFPT.Models.Queries;

namespace TrainingFPT.Controllers
{
    public class TopicsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string SearchString, string Status)
        {
            TopicsViewModel topicViewModel = new TopicsViewModel();
            topicViewModel.TopicDetailList = new List<TopicDetail>();
            var dataTopics = new TopicQuery().GetAllDataTopics(SearchString, Status);
            foreach (var data in dataTopics)
            {
                topicViewModel.TopicDetailList.Add(new TopicDetail
                {
                    Id = data.Id,
                    NameTopic = data.NameTopic,
                    CourseId = data.CourseId,
                    Description = data.Description,
                    ViewVideoTopic = data.ViewVideoTopic,
                    ViewAudioTopic = data.ViewAudioTopic,
                    ViewDocumentTopic = data.ViewDocumentTopic,
                    Status = data.Status,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt,
                    viewCourseName = data.viewCourseName
                });
            }
            ViewData["keyword"] = SearchString;
            ViewBag.Status = Status;
            return View(topicViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            TopicDetail topic = new TopicDetail();
            List<SelectListItem> items = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var course in dataCourses)
            {
                items.Add(new SelectListItem
                {
                    Value = course.Id.ToString(),
                    Text = course.NameCourse
                });
            }
            ViewBag.Courses = items;
            return View(topic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TopicDetail topic, IFormFile Video, IFormFile Audio, IFormFile DocumentTopic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //return Ok(topic);
                    string videoTopic = UploadFileHelper.UploadFile(Video);
                    string audioTopic = UploadFileHelper.UploadFile(Audio);
                    string documentTopic = UploadFileHelper.UploadFile(DocumentTopic);

                    int idTopic = new TopicQuery().InsertDataTopic(
                        topic.NameTopic,
                        topic.CourseId,
                        topic.Description,
                        videoTopic,
                        audioTopic,
                        documentTopic,
                        topic.Status
                    );
                    if (idTopic > 0)
                    {
                        TempData["saveStatus"] = true;
                    }
                    else
                    {
                        TempData["saveStatus"] = false;
                    }
                    return RedirectToAction(nameof(TopicsController.Index), "Topics");
                }
                catch (Exception ex)
                {
                    //neu co loi
                    return Ok(ex.Message);
                }
            }
            List<SelectListItem> items = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var course in dataCourses)
            {
                items.Add(new SelectListItem
                {
                    Value = course.Id.ToString(),
                    Text = course.NameCourse
                });
            }
            ViewBag.Courses = items;
            return View(topic);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            bool del = new TopicQuery().DeleteItemTopic(id);
            if (del)
            {
                TempData["statusDel"] = true;
            }
            else
            {
                TempData["statusDel"] = false;
            }
            return RedirectToAction(nameof(TopicsController.Index), "Topics");
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            TopicDetail detail = new TopicQuery().GetDataTopicById(id);
            List<SelectListItem> items = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var course in dataCourses)
            {
                items.Add(new SelectListItem
                {
                    Value = course.Id.ToString(),
                    Text = course.NameCourse
                });
            }
            ViewBag.Courses = items;
            return View(detail);

        }
        [HttpPost]
        public IActionResult Edit(TopicDetail topicDetail, IFormFile Video, IFormFile Audio, IFormFile DocumentTopic)
        {
            try
            {
                var detail = new TopicQuery().GetDataTopicById(topicDetail.Id);
                string videoTopic = detail.ViewVideoTopic;
                string audioTopic = detail.ViewAudioTopic;
                string documentTopic = detail.ViewDocumentTopic;
                // kiem tra xe nguoi co muon thay doi anh ko?
                if (topicDetail.Video != null)
                {
                    // co thay doi anh
                    videoTopic = UploadFileHelper.UploadFile(Video);
                }
                if (topicDetail.Audio != null)
                {
                    audioTopic = UploadFileHelper.UploadFile(Audio);
                }
                if (topicDetail.DocumentTopic != null)
                {
                    documentTopic = UploadFileHelper.UploadFile(DocumentTopic);
                }
                bool update = new TopicQuery().UpdateTopicById(
                                topicDetail.NameTopic,
                                topicDetail.CourseId,
                                topicDetail.Description,
                                videoTopic,
                                audioTopic,
                                documentTopic,
                                topicDetail.Status,
                                topicDetail.Id
                              );
                if (update)
                {
                    TempData["updateStatus"] = true;
                }
                else
                {
                    TempData["updateStatus"] = false;
                }
                return RedirectToAction(nameof(TopicsController.Index), "Topics");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            //List<SelectListItem> items = new List<SelectListItem>();
            //var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            //foreach (var course in dataCourses)
            //{
            //    items.Add(new SelectListItem
            //    {
            //        Value = course.Id.ToString(),
            //        Text = course.NameCourse
            //    });
            //}
            //ViewBag.Course = items;
            //return View(topicDetail);

        }


    }
}
