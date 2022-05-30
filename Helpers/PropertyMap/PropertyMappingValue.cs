using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Helpers.PropertyMap
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool IsRevert { get; set; }
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool isRevert = false)
        {
            DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            IsRevert = isRevert;
        }
    }
}
