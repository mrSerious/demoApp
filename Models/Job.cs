using System;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Display(Name = "Job Status")]
        public bool IsComplete { get; set; }
        [Display(Name = "Assigned To")]
        public virtual User AssignedTo { get; set; }

        public override string ToString()
        {
            return "Job [id=" + this.JobId + ", title=" + this.Title + ", dueDate=" + this.DueDate.ToString() + ", IsComplete=" + this.IsComplete + "]";
        }
    }
}