using System;
namespace MvvmMobile.iOS.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StoryboardAttribute : Attribute
    {
        public StoryboardAttribute(string storyboardName, string storyboardId)
        {
            StoryboardName = storyboardName;
            StoryboardId = storyboardId;
        }

        public string StoryboardName { get; private set; }
        public string StoryboardId { get; private set; }
    }
}