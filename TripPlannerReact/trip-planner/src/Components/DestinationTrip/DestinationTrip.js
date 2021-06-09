import React, {useState, useEffect} from 'react';

let DestinationsTable = (props) => 
{
    const [state, setState] = useState([]);
    const [destinationTrip, setDestinationTrip] = useState({});
    const [destinationTrips, setDestinationTrips] = useState([]);
    const [updateDestinationTrips, setUpdateDestinationTrips] = useState([]);

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
    //getting all of the destinationTrips for the edit.  
    useEffect(() => {
        const headers = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${props.token}` 
            }
        };
        let url = "https://localhost:44365/api/destinationtrips/bytrip/" + props.tripID;
        fetch(url, headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setUpdateDestinationTrips(json))
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

    let handleUpdate = (event) => {
        let index = destinationTrips.findIndex((v) => v.destinationID === event.target.id);
        if(index >= 0){
          destinationTrips[index].description = event.target.value;
          setDestinationTrips(destinationTrips);
        }
    }

    let handleEditUpdate = (event) => {
        let index = updateDestinationTrips.findIndex((v) => v.destinationID === event.target.id);
        if(index >= 0){
          updateDestinationTrips[index].description = event.target.value;
          setUpdateDestinationTrips(updateDestinationTrips);
        }
    }

    let handleDelete = (id) => {
        let index = destinationTrips.findIndex((v) => v.destinationID == id);
        let dt = [...destinationTrips];
        dt.splice(index,1);
        setDestinationTrips(dt);
    }

    // let handleEditDelete = (id) => {
    //     let newDT = updateDestinationTrips.filter((v) => v.destinationID === id);
    //     setUpdateDestinationTrips(newDT);
    // }

    let handleSubmit = (event) => {
        event.preventDefault();
        let dt = {
            destinationID: destinationTrip.destinationID,
            destination: destinationTrip.destination,
            description: destinationTrip.description
        };
        let newDestinationTrips = [...destinationTrips];
        newDestinationTrips.push(dt);
        setDestinationTrips(newDestinationTrips);
        props.addDestinations(newDestinationTrips);
        event.target.reset();
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
            return list.map((destination, index) => {
                return (
                    <tr key={index}>
                        <td>
                            <button onClick={() => handleDelete(destination.destinationID)} id="deleteButton" type="button" className="btn btn-danger">-</button>
                            {destination.destinationID}
                        </td>
                        <td>
                            {destination.destination}
                        </td>
                        <td>
                            <input type="text" className="form-control inputs editInput" id={destination.destinationID} defaultValue={destination.description} onChange={handleUpdate}/>
                        </td>
                    </tr>
                );
            });
        }

    }

    let editDestinations = (list) =>
    {
        console.log(updateDestinationTrips);
        if(list !== null){
            return list.map((destination, index) => {
                return (
                    <tr key={index}>
                        <td>
                        {/* onClick={handleDelete(destination.destinationID)}  */}
                            <button id="deleteButton" type="button" className="btn btn-danger">-</button>
                            {destination.destinationID}
                        </td>
                        <td>
                            {destination.destination}
                        </td>
                        <td>
                            <input type="text" className="form-control inputs" id={destination.destinationID} defaultValue={destination.description} onChange={handleEditUpdate}/>
                        </td>
                    </tr>
                );
            });
        }

    }
    if(props.isAdd){
        return (
            <div class="container">
                <form onSubmit={handleSubmit} id="destinationTripForm" >
                    <table className="table table-striped" id="destinationTable">
                        <thead className="thead-dark">
                            <tr key={-1}>
                                <th ></th>
                                <th><h5 id="destinationHeader">DESTINATIONS</h5></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr key={-2}>
                                <td>
                                    <div className="form-group">
                                        <label htmlfor="Description" className="control-label">Description</label>
                                        <input id="descriptionInput" form="destinationTripForm" type="text" htmlfor="Description" className="form-control inputs" onChange={handleChange}/>
                                    </div>
                                </td>
                                <td>
                                    <div className="form-group">
                                        <label htmlfor="Destinations" className="control-label">Destinations</label>
                                        <select onChange={handleSelection} form="destinationTripForm">
                                            <option selected disabled>Choose Destination</option>
                                            {destinationOptions(state)}
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <button form="destinationTripForm" id="addButton" type="submit" className="btn btn-primary">&#43;</button>
                                </td>
                            </tr>
                            {addedDestinations(destinationTrips)}
                        </tbody>
                    </table>
                </form>
            </div>
        );
    }
    else{
        return (
            <div class="container">
                <form>
                    <table className="table table-striped" id="destinationTable">
                        <thead className="thead-dark">
                            <tr>
                                <th></th>
                                <th><h5 id="destinationHeader">DESTINATIONS</h5></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {editDestinations(updateDestinationTrips)}
                        </tbody>
                    </table>
                </form>
            </div>
        );
    }
    
}
export default DestinationsTable;