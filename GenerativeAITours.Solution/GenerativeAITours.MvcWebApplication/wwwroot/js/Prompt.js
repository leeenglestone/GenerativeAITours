var _location = '';
var _interests = '';
var _days = 0;
var _prompt = '';

const Prompt =
{
    SetLocation: function SetLocation(location) {
        _location = location;
        this.CreatePrompt();
    },

    SetDays: function SetDays(days) {
        _days = days;
        this.CreatePrompt();
    },

    SetInterest: function SetInterest(interest) {
        _interests = UI.GetCheckedInterests();
        this.CreatePrompt();
    },

    CreatePrompt: function CreatePrompt() {
        //if (_location == '') {
        //    alert('Please select a location');
        //    return;
        //}

        _prompt = 'Suggest places to visit in ' + _location + '.';

        if (_days > 0) {
            _prompt += ' Create a ' + _days + ' day itinerary containing 3 activities per day.';
        }

        if (_interests != '') {
            _prompt += ' For people with interests in ' + _interests + ', with a brief descriptions of each place.';
        }
       
        _prompt += ' Return in json format, a property for location(' + _location + '), another property for duration(' + _days + '), another for an array of interests (' + _interests + '), then another for days as an array containing day objects (each with an increasing dayNumber property and an array of activities and each activity with its own name, description and latitude, longitude properties).';
        //_prompt += 'Wrap names of locations in the description property in double angle brackets ([[]])';

        navigator.clipboard.writeText(_prompt);

        document.getElementById('lblPrompt').innerText = _prompt;
    }
}