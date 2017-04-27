using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace TransparentWindow
{
    public class FavouriteLocation
    {
        private List<Location> _locations;
        private const string FavoriteLocationDataFile = "FavoriteLocation.json";

        Form _transparentForm;
        AttachedWindow _attachedWindow;

        public FavouriteLocation(Form transparentForm, AttachedWindow attachedWindow)
        {
            _transparentForm = transparentForm;
            _attachedWindow = attachedWindow;

        }

        public void SaveCurrentLocation(string FileName)
        {
            var location = new TransparentWindow.Location();
            location.TransparentWindowLocation = _transparentForm.Bounds;
            location.AttachedWindowLocation = _attachedWindow.GetAttacheWindowSize();

            var serializer = new JavaScriptSerializer();
            File.WriteAllText(FileName + ".json", serializer.Serialize(location));
        }


        public IEnumerable<Location> GetLocations()
        {
            LoadLocations();
            return _locations;
        }

        private void LoadLocations()
        {
            if (System.IO.File.Exists(FavoriteLocationDataFile))
            {
                _locations = new List<Location>();
                return;
            }

            var json = File.ReadAllText(FavoriteLocationDataFile);
            var serializer = new JavaScriptSerializer();
            _locations = serializer.Deserialize<List<Location>>(json);
        }

        private void SaveLocations()
        {
            var serializer = new JavaScriptSerializer();
            File.WriteAllText(FavoriteLocationDataFile, serializer.Serialize(_locations));
        }

        public void MoveToLocation(string locationFile)
        {
            var json = File.ReadAllText(locationFile);
            var serializer = new JavaScriptSerializer();
            var location = serializer.Deserialize<Location>(json);
            _transparentForm.Bounds = location.TransparentWindowLocation;
            _attachedWindow.MoveResizeAttacheWindow(location.AttachedWindowLocation);
        }
    }
    public class Location
    {
        public Rectangle TransparentWindowLocation { get; set; }
        public Rectangle AttachedWindowLocation { get; set; }
    }
}
