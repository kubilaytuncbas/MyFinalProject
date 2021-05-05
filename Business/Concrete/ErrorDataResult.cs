using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete
{
    internal class ErrorDataResult<T> : IDataResult<User>
    {
        private object passwordError;

        public ErrorDataResult(object passwordError)
        {
            this.passwordError = passwordError;
        }

        public User Data => throw new System.NotImplementedException();

        public bool Success => throw new System.NotImplementedException();

        public string Message => throw new System.NotImplementedException();
    }
}