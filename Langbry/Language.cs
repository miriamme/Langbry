using Newtonsoft.Json;

namespace Langbry
{
    /// <summary>
    /// 언어 데이터
    /// </summary>
    /// <remarks>
    /// 언어정보가 있는 json 파일로부터 초기화된다.
    /// </remarks>
    internal class Language
    {
        /// <summary>
        /// 번역코드
        /// </summary>
        [JsonProperty("code")]
        internal string Code { get; set; } = "";

        /// <summary>
        /// 한국어
        /// </summary>
        [JsonProperty("ko")]
        internal string KO { get; set; } = "";

        /// <summary>
        /// 영어
        /// </summary>
        [JsonProperty("en")]
        internal string EN { get; set; } = "";

        /// <summary>
        /// 태국어
        /// </summary>
        [JsonProperty("th")]
        internal string TH { get; set; } = "";

        /// <summary>
        /// 인도네시아어
        /// </summary>
        [JsonProperty("id")]
        internal string ID { get; set; } = "";

        // todo: 언어가 늘어날 경우 추가하세요.
    }
}