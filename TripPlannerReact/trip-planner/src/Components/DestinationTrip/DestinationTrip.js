import React, {useState, useEffect} from 'react';

let DestinationsTable = (props) => 
{
    const [state, setState] = useState([]);
    const [destinationTrip, setDestinationTrip] = useState({});
    const [destinationTrips, setDestinationTrips] = useState([]);

    useEffect(() => {
      const headers = {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": `Bearer ${props.token}` 
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

    let handleChange = (event) => {

        let newDestinationTrip = { ...destinationTrip };

        newDestinationTrip["description"] = event.target.value;
        setDestinationTrip(newDestinationTrip);
    }

    let handleSelection = (event) => {
        
        let newDestinationTrip = { ...destinationTrip };
        newDestinationTrip["destinationID"] = event.target.value;
        newDestinationTrip["destination"] = event.target.options[event.target.selectedIndex].text;
        setDestinationTrip(newDestinationTrip);
    }

    let handleSubmit = () => {
        
        let dt = {
            destinationID: destinationTrip.destinationID,
            destination: destinationTrip.destination,
            description: destinationTrip.description
        };
        let newDestinationTrips = [...destinationTrips];
        newDestinationTrips.push(dt);
        setDestinationTrips(newDestinationTrips);
    }
    let destinationOptions = (list) =>
    {
        return list.map((destination) => {
            return(
                <option name="id" value={destination.destinationID} >{destination.city + " - " + destination.country}</option>
                
            );
        });

    }

    let addedDestinations = (list) =>
    {
        if(list !== null){
            return list.map((destination) => {
                return (
                    <tr>
                        <td>
                            {destination.destinationID}
                        </td>
                        <td>
                            {destination.destination}
                        </td>
                        <td>
                            {destination.description}
                        </td>
                    </tr>
                );
            });
        }

    }
    return (
        <div>
            <form>
                <table className="table table-striped" id="destinationTable">
                    <thead className="thead-dark">
                        <tr>
                            <th></th>
                            <th>DESTINATIONS</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <div className="form-group">
                                    <label htmlfor="Description" className="control-label">Description</label>
                                    <input type="text" htmlfor="Description" className="form-control" onChange={handleChange}/>
                                </div>
                            </td>
                            <td>
                                <div className="form-group">
                                    <label htmlfor="Destinations" className="control-label">Destinations</label>
                                    <select onChange={handleSelection}>
                                        <option selected disabled>Choose Destination</option>
                                        {destinationOptions(state)}
                                    </select>
                                </div>
                            </td>
                            <td>
                                <button onClick={handleSubmit} type="button" className="btn btn-primary">&#43;</button>
                            </td>
                        </tr>
                        {addedDestinations(destinationTrips)}
                    </tbody>
                </table>
            </form>
        </div>
    );
}
export default DestinationsTable;