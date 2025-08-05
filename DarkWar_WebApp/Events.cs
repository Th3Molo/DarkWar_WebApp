namespace DarkWar_WebApp
{
    public class Events
    {
        #region Properties
        public int ID { get; set; } //PK
        public string Eventname { get; set; } = string.Empty;
        public bool HaveParticipated { get; set; } = false;
        public double PointsGet { get; set; } = 0;
        #endregion

        #region Constructor
        public Events() { }
        private Events(string eventname, bool haveparticipated, double points) 
        {
            Eventname = eventname;
            HaveParticipated = haveparticipated;
            PointsGet = points;
        }
        #endregion

        #region Methods
        public static Events AddEvent(string eventname, bool haveparticipated, double points)
        {
            return new Events(eventname, haveparticipated, points);
        }
        #endregion
    }
}
