using System;
using System.Collections.Generic;

namespace JP.Base.DAL.ADO.EntityMappers.DictionaryMapping
{
    public class EntityMappings
    {
        private static EntityMappings instance;
        private IDictionary<Type, IDictionary<string, string>> mappings;

        private EntityMappings()
        {
            mappings = new Dictionary<Type, IDictionary<string, string>>();

            ConfigureMappings(ref mappings);
        }

        public IDictionary<string, string> Get<T>()
        {
            if (mappings.ContainsKey(typeof(T)))
                return mappings[typeof(T)];
            else
                return null;
        }

        public EntityMappings Instance()
        {
            instance = instance ?? new EntityMappings();

            return instance;
        }

        protected virtual void ConfigureMappings(ref IDictionary<Type, IDictionary<string, string>> mappings)
        {
            throw new NotImplementedException("You must inherit from EntityMapping class and implement this method first!");
        }
    }
}