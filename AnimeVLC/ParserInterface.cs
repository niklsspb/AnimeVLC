using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeVLC
{
    public interface ParserInterface
    {
        String url { get; set; }
        List<Anime> getUrl(string url);
        String getVideoUrl(string url);


    }
}
