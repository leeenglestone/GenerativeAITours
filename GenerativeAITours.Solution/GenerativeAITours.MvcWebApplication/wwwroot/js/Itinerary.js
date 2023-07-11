const Itinerary =
{
    Show: function showItinerary(itinerary) {

        console.log(itinerary);

        document.getElementById('btnCreateItinerary').style.display = "none";

        let activityNumber = 1;

        const lblResponse = document.getElementById("lblResponse");

        // For each day
        itinerary.days.forEach(day => {

            const dayNumberHeading = document.createElement("h3");
            dayNumberHeading.textContent = `Day ${day.dayNumber} `;
            lblResponse.appendChild(dayNumberHeading);

            // For each activity
            day.activities.forEach(activity => {

                activity.number = activityNumber;
                activityNumber++;

                const col = document.createElement("div");
                col.className = "col-4";

                const cardContainer = document.createElement("div");
                cardContainer.className = "card";

                const image = document.createElement("img");
                image.className = "card-img-top";
                image.src = '/activities/' + itinerary.location + ' ' + activity.name + '.jpg';

                const cardBody = document.createElement("div");
                cardBody.className = "card-body";

                const activityHeading = document.createElement("h5");
                activityHeading.innerText = activity.number + '. ' + activity.name;

                const cardText = document.createElement("p");
                cardText.className = "card-text";
                cardText.innerText = activity.description;

                const mapButton = document.createElement("button");
                mapButton.type = "button";
                mapButton.innerText = "View on Map";
                mapButton.className = "btn btn-secondary";
                mapButton.style.margin = "3px";
                mapButton.onclick = function () {
                    TourMap.ShowActivity(activity.name);
                };

                const infoButton = document.createElement("button");
                infoButton.type = "button";
                infoButton.innerText = "More Information";
                infoButton.className = "btn btn-secondary";
                infoButton.style.margin = "3px";                
                infoButton.setAttribute("data-bs-toggle", "modal");
                infoButton.setAttribute("data-bs-target", "#staticBackdrop");
                infoButton.onclick = function () {

                    
                    document.getElementById("activityModalHeading").innerText = activity.name;

                    // Replace with longer description
                    document.getElementById("activityModalDescription").innerText = activity.description;
                };

                col.appendChild(cardContainer);

                cardContainer.appendChild(image);
                cardContainer.appendChild(cardBody);

                cardBody.appendChild(activityHeading);
                cardBody.appendChild(cardText);
                cardBody.appendChild(mapButton);
                cardBody.appendChild(infoButton);

                lblResponse.appendChild(col);

            });

        });
    },

    CreateItinerary: function createItinerary() {

        if (_location === '') {
            alert('Please select a location');
            return;
        }

        // Clear any existing results
        // Disable button and change text
        document.getElementById('results-heading').innerHTML = '';
        document.getElementById('lblResponse').innerHTML = '';

        document.getElementById('btnCreateItinerary').innerText = 'Generating itinerary..';
        document.getElementById('btnCreateItinerary').disabled = true;

        const postData = async (url, data) => {
            try {
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

                console.log('Data sent:', data);

                const result = await response.json();
                console.log('Post successful:', result);

                // Show results heading
                document.getElementById('results-heading').innerHTML = 'Itinerary for a ' + _days + ' day visit to ' + _location + ' <br/>for those with an interest in ' + _interests;

                let text = result;

                //showItinerary(text);
                Itinerary.Show(text);

                TourMap.PlotLocations(text);

                // Set button back to normal
                document.getElementById('btnCreateItinerary').innerText = 'Create itinerary..';
                document.getElementById('btnCreateItinerary').disabled = false;

            } catch (error) {
                console.error('Error:', error);
            }
        };

        // Usage
        const apiUrl = Config.GenerativeToursApiUrl(); //'https://localhost:7234/tour';
        const stringData = _prompt;

        postData(apiUrl, stringData);
    }
}