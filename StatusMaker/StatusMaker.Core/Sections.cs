namespace StatusMaker.Core
{
    public class Section
    {
        public Section(string heading, string dataFilter)
        {
            this.Heading = heading;
            this.DataFilter = dataFilter;
        }

        public string Heading { get; private set; }
        public string DataFilter { get; private set; }
    }

    public class Sections
    {
        public static Section InProgress
        {
            get
            {
                return new Section("In Progress", " Regression='No' and Category='In Progress'");
            }
        }
    }
}
