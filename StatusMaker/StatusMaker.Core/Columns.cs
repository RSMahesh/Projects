namespace StatusMaker.Core
{
    public class Column
    {
        public Column(string dataColumnName, string placeHolderName)
        {
            this.DataColumnName = dataColumnName;
            this.PlaceHolderName = placeHolderName;
        }

        public string DataColumnName { get; private set; }
        public string PlaceHolderName { get; private set; }
    }

    public class Columns
    {
        public static Column JiraNumber
        {
            get
            {
                return new Column("JIRA #", "JIRA #");
            }
        }

        public static Column Comments
        {
            get
            {
                return new Column("Comments", "Comments");
            }
        }


        public static Column Description
        {
            get
            {
                return new Column("Description", "Description");
            }
        }


        public static Column EPIC
        {
            get
            {
                return new Column("EPIC", "EPIC");
            }
        }


        public static Column Priority
        {
            get
            {
                return new Column("Priority", "Priority");
            }
        }

        public static Column PRNumber
        {
            get
            {
                return new Column("PR #", "PR #");
            }
        }


        public static Column Status
        {
            get
            {
                return new Column("Status", "Status");
            }
        }

        public static Column Author
        {
            get
            {
                return new Column("Author", "Author");
            }
        }

        public static Column Tester
        {
            get
            {
                return new Column("Tester", string.Empty);
            }
        }

        public static Column Review
        {
            get
            {
                return new Column("Review", string.Empty);
            }
        }
    }

}
