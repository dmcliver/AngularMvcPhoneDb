namespace NHibernate.Infrastructure
{
    public class Hbm2Ddl
    {
        private readonly string operation;

        private Hbm2Ddl(string operation)
        {
            this.operation = operation;
        }

        /// <summary>
        /// Create the database schema from the fluent nhibernate mappings
        /// </summary>
        public static Hbm2Ddl Create
        {
            get
            {
                return new Hbm2Ddl("create");
            }
        }

        /// <summary>
        /// Update the database schema from the fluent nhibernate mappings
        /// </summary>
        public static Hbm2Ddl Update
        {
            get
            {
                return new Hbm2Ddl("update");
            }
        }

        /// <summary>
        /// Validate the database schema from the fluent nhibernate mappings
        /// </summary>
        public static Hbm2Ddl Validate
        {
            get
            {
                return new Hbm2Ddl("validate");
            }
        }

        /// <summary>
        /// Don't perform any database operation
        /// </summary>
        public static Hbm2Ddl None
        {
            get
            {
                return new Hbm2Ddl("none");
            }
        }

        public static Hbm2Ddl CreateDrop
        {
            get
            {
                return new Hbm2Ddl("create-drop");
            }
        }

        public override string ToString()
        {
            return operation;
        }
    }
}