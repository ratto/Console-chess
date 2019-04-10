using System;

namespace Board
{
    class BoardException : ApplicationException
    {
        public BoardException(string msg): base(msg)
        {

        }
    }
}
