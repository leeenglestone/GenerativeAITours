const UI = {

    AddCityButtons: function (elementId) {
        const cities = Tour.cityDestinations();
        this.AddLocationButtons(elementId, cities);
    },
    AddCountryButtons: function (elementId) {
        const countries = Tour.countryDestinations();
        this.AddLocationButtons(elementId, countries);
    },
    AddLocationButtons: function (elementId, locations) {
        var container = document.getElementById(elementId);

        // Loop through the interests array
        for (let i = 0; i < locations.length; i++) {
            // Create a new input element
            const locationRadio = document.createElement('input');

            // Set the checkbox attributes
            locationRadio.type = 'radio';
            locationRadio.className = 'btn-check';
            locationRadio.id = 'radio-' + locations[i].toLowerCase().replace(/\s/g, '-');
            locationRadio.autocomplete = 'off';
            locationRadio.name = 'location';
            locationRadio.value = locations[i];

            // Add onclick event listener to the checkbox
            locationRadio.onclick = function () {
                Prompt.SetLocation(locations[i]);
            };

            // Create a new label element
            const label = document.createElement('label');
            label.className = 'btn btn-outline-primary btn-space';

            // Set the label text to the current interest
            label.textContent = locations[i];
            label.htmlFor = locationRadio.id;

            // Append the checkbox and label to the container element
            container.appendChild(locationRadio);
            container.appendChild(label);
        }
    },
    AddInterestButtons: function (elementId) {
        const container = document.getElementById(elementId);

        // Access the array of interests
        const interests = Tour.interests();

        // Loop through the interests array
        for (let i = 0; i < interests.length; i++) {
            // Create a new input element
            const checkbox = document.createElement('input');

            // Set the checkbox attributes
            checkbox.type = 'checkbox';
            checkbox.className = 'btn-check';
            checkbox.id = 'checkbox-' + interests[i].toLowerCase().replace(/\s/g, '-');
            checkbox.autocomplete = 'off';
            checkbox.name = 'interest';
            checkbox.value = interests[i];

            // Add onclick event listener to the checkbox
            checkbox.onclick = function () {
                Prompt.SetInterest(interests[i]);
            };

            // Create a new label element
            const label = document.createElement('label');
            label.className = 'btn btn-outline-primary btn-space';

            // Set the label text to the current interest
            label.textContent = interests[i];
            label.htmlFor = checkbox.id;

            // Append the checkbox and label to the container element
            container.appendChild(checkbox);
            container.appendChild(label);
        }
    },
    AddDurationButtons: function addDurationButtons(elementId) {

        const container = document.getElementById(elementId);
        const durations = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14];

        for (let i = 0; i < durations.length; i++) {

            const radio = document.createElement('input');
            radio.id = 'duration-' + durations[i];
            radio.className = 'btn-check';
            radio.name = 'days';
            radio.autocomplete = 'off';
            radio.type = 'radio';
            radio.onclick = function () {
                Prompt.SetDays(durations[i]);
            };

            const label = document.createElement('label');
            label.className = 'btn btn-outline-primary btn-space';
            label.htmlFor = radio.id;
            label.textContent = durations[i];

            container.appendChild(radio);
            container.appendChild(label);
        }
    },
    GetCheckedInterests: function getCheckedInterests() {
        const checkboxes = document.querySelectorAll('input[name="interest"]:checked');
        const checkedValues = Array.from(checkboxes).map(checkbox => checkbox.value);
        return checkedValues.join(', ');
    }
};