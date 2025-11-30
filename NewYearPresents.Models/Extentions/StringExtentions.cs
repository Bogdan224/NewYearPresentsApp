using System.Text;

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
            while ((source[cnt] < 'А' || source[cnt] > 'я') && (source[cnt] < 'A' || source[cnt] > 'z'))
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
            source = source.Remove(source.Length-cnt, cnt);

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

        public static float GetFloat(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                throw new NullReferenceException(nameof(source));

            int length = 0;

            bool flag = false;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] >= '0' && source[i] <= '9')
                    break;
                length++;
            }

            if (length != 0)
            {
                source = source.Remove(0, length);
                length = 0;
            }

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == ',' || source[i] == '.')
                {
                    if (flag) break;
                    flag = true;
                }
                else if (!(source[i] >= '0' && source[i] <= '9'))
                    break;

                length++;
            }

            if (length == 0)
                throw new ArgumentNullException(source);

            return Convert.ToSingle(source.Substring(0, length));
        }

        public static float GetFloat(this string? source, int lastIndex)
        {
            if(string.IsNullOrEmpty(source))
                throw new NullReferenceException(nameof(source));

            bool isFirstSpace = false;
            source = source.Replace('.', ',');
            int length = 0;
            do
            {
                length++;
                lastIndex--;
                if (source[lastIndex] == ' ' && !isFirstSpace) {
                    lastIndex--;
                    isFirstSpace = true;
                }
                if (source[lastIndex] == ',' && !(source[lastIndex - 1] >= '0' && source[lastIndex - 1] <= '9')) break;
                if (!isFirstSpace)
                    isFirstSpace = true;
            }
            while ((source[lastIndex] >= '0' && source[lastIndex] <= '9') || source[lastIndex] == ',');

            try
            {
                return Convert.ToSingle(source.Substring(lastIndex + 1, length - 1));
            }
            catch { throw new ArgumentException(source + " " + nameof(lastIndex) + ": " + (lastIndex + length).ToString()); }
        }
    }
}