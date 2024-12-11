namespace Business.Exceptions
{
    public class MusicLibraryException : Exception
    {
        public MusicLibraryException() { }
        public MusicLibraryException(string message) : base(message) { }
        public MusicLibraryException(string message,  Exception innerException) : base(message, innerException) { }
    }
}