using System;
using Models.Error.Base;

namespace Models.Error
{
    public class CreationException:BaseException
    {
        private readonly string _entity;

        public CreationException(Type entityType)
        {
            _entity = entityType.Name;
        }
        public override string Message => base.Message + $" Couldn't create entity '{_entity}'";
    }
}