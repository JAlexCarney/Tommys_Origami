import './App.css';
import React, {useState, useEffect} from 'react';
import './css/TripsTable.css';
import './css/ReviewsTable.css';
import TripPlanner from './Components/TripPlanner';
import DestinationTrip from './Components/DestinationTrip/DestinationTrip';

function App() {
  const [state, setState] = useState([]);

  useEffect(() => {
    const headers = {
      method: "GET",
      headers: {
          "Accept": "application/json",
          // "Authorization": `Bearer ${token}` 
      }
  };
  
    fetch("https://localhost:44365/api/reports/mostvisited", headers)
        .then(response => {
            if (response.status !== 200) {
                console.log(`Bad status: ${response.status}`);
                return Promise.reject("response is not 200 OK");
            }
            return response.json();
        })
        .then(json => setState(json))
        .catch(console.log);
  }, []);
  return (
    <div>
      <TripPlanner />
    </div>
  );
}

export default App;
