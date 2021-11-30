using System;
using Models.Error.Base;

namespace Models.Error
{
    public class NotFoundException: BaseException
    {
        private readonly string _entity;

        public NotFoundException(Type entityType)
        {
            _entity = entityType.Name;
        }
        public override string Message => base.Message + $" Couldn't found entity '{_entity}'";
    }
}