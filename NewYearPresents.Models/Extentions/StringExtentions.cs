namespace NewYearPresents.Models.Extentions
{
    public static class StringExtensions
    {
        public static string NormalizeText(this string? source)
        {
            source = source.RemoveNonLetterCharacterFromBeginning();
            source = source.RemoveSpacesFromEnding();
            source = source.NormalizeWhiteSpacing();
            source = source.ToUpperFirstLetter();

            return source;
        }

        public static string RemoveNonLetterCharacterFromBeginning(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            int cnt = 0;
            while (source[cnt] < 'A' || source[cnt] > 'z')
            {
                cnt++;
            }
            source = source.Remove(0, cnt);

            return source;
        }

        public static string RemoveSpacesFromEnding(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            int cnt = 0;
            while (source[source.Length - 1 - cnt] == ' ')
            {
                cnt++;
            }
            source = source.Remove(source.Length, cnt);

            return source;
        }

        public static string NormalizeWhiteSpacing(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == ' ')
                {
                    int cnt = i;
                    while (cnt + 1 < source.Length && source[cnt + 1] == ' ')
                    {
                        cnt++;
                    }
                    source = source.Remove(i + 1, cnt - i);
                }
            }

            return source;
        }

        public static string ToUpperFirstLetter(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            source = source.ToLower();
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
    }
}