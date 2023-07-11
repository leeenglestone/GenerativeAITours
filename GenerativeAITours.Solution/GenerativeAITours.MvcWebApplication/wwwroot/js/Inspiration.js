var _interests = '';
var _prompt = '';

const Inspiration = {

    SetInterest: function SetInterest() {
        this.CreatePrompt();
    },

    CreatePrompt: function CreatePrompt() {

        _interests = UI.GetCheckedInterests();

        _prompt = 'Suggest ten holiday destinations for someone, considering their interests in ' + _interests + ' with a short description of why for each.'

        //_prompt += 'Return in json format, a property for location(' + _location + '), another property for duration(' + _days + '), another for interest(' + _interest + '), then another for days as an array containing day objects (each with a day number and array of activities).';
        //_prompt += 'Wrap locations in double angle brackets ([[]])';

        navigator.clipboard.writeText(_prompt);

        document.getElementById('lblPrompt').innerText = _prompt;
    },

    SuggestLocations: function SuggestLocations() {

        if (_interests === '') {
            alert('Please select interests');
            return;
        }

        const postData = async (url, data) => {
            try {

                // Disable button and change text
                document.getElementById('btnSuggestLocations').innerText = 'Generating..';
                document.getElementById('btnSuggestLocations').disabled = true;

                document.getElementById('lblPrompt').innerText = '';

                const response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });

                if (!response.ok) {
                    throw new Error('Error posting data to API');
                }

                const result = await response.json();
                
                let text = result.choices[0].text;

                this.ShowItinerary(text);

                // Set button back to normal
                document.getElementById('btnSuggestLocations').innerText = 'Suggest locations..';
                document.getElementById('btnSuggestLocations').disabled = false;

            } catch (error) {
                console.error('Error:', error);
            }
        };

        const apiUrl = Config.GenerativeToursApiUrl();
        const stringData = _prompt;

        postData(apiUrl, stringData);
    },

    ShowItinerary: function showItinerary(text) {
        var div = document.getElementById("lblResponse");

        // Split the text into individual lines
        var lines = text.split("\n");

        // Create a variable to store the HTML content
        var htmlContent = "";

        // Iterate through each line
        for (var i = 0; i < lines.length; i++) {
            var line = lines[i];

            // Check if the line starts with "Day "
            if (line.startsWith("Day ")) {
                // Add the line with bold formatting
                htmlContent += "<strong>" + line + "</strong>";
            } else {
                // Add the line without any formatting
                htmlContent += line;
            }

            // Add a line break after each line
            htmlContent += "<br>";
        }

        // Set the HTML content of the div
        div.innerHTML = htmlContent;
    }
}