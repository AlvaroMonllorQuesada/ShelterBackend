using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.ValueObjects
{
    public class AnimalImage
    {
        public string Url { get; private set; } = string.Empty;
        public bool IsPrimary { get; private set; } = false;

        private AnimalImage() { }

        public AnimalImage(string url, bool isPrimary = false)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("Image URL is required.");
            Url = url;
            IsPrimary = isPrimary;
        }

        public void SetAsSecondary() => IsPrimary = false;
    }
}
