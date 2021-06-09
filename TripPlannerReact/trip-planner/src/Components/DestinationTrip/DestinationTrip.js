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

    let onEditChange = (event) => {
        let index = updateDestinationTrips.findIndex((v) => v.destinationID == event.target.id);
        if(index >= 0){
            let newDT = [...updateDestinationTrips];
            newDT[index].description = event.target.value;
            setUpdateDestinationTrips(newDT);
            props.editDestinations(newDT);
        }
    }

    let handleDelete = (id) => {
        let index = destinationTrips.findIndex((v) => v.destinationID == id);
        let dt = [...destinationTrips];
        dt.splice(index,1);
        setDestinationTrips(dt);
    }

    let deleteDestinationTrip = (id) => {
    const init = {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + props.token
        },
        body: JSON.stringify({
            "tripID":props.tripID,
            "destinationID": id
        })
      };
  
    fetch(`https://localhost:44365/api/destinationtrips`, init)
        .then(response => {
            if (response.status !== 200) {
                return Promise.reject("response is not 200 OK");
            }
        })
        .catch(console.log);
    }

    let handleEditDelete = (id) => {
        deleteDestinationTrip(id);
        let index = updateDestinationTrips.findIndex((v) => v.destinationID == id);
        let dt = [...updateDestinationTrips];
        dt.splice(index,1);
        setUpdateDestinationTrips(dt);
    }

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

    let addNewDestinationTrip = (dt) => {
        let newDT = {
            tripID: props.tripID,
            destinationID: dt.destinationID,
            description: dt.description
        }
        const init = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + props.token
            },
            body: JSON.stringify(newDT)
          };
          fetch("https://localhost:44365/api/destinationtrips", init)
          .then(response => {
              if (response.status !== 201) {
                  return Promise.reject("response is not 200 OK");
              }
              return response.json();
          })
          .then((console.log))
          .catch(console.log);
        
    }

    let handleEditSubmit = (event) => {
        event.preventDefault();
        let dt = {
            destinationID: destinationTrip.destinationID,
            destination: destinationTrip.destination,
            description: destinationTrip.description
        };
        let newDestinationTrips = [...updateDestinationTrips];
        newDestinationTrips.push(dt);
        setUpdateDestinationTrips(newDestinationTrips);
        props.editDestinations(newDestinationTrips);
        event.target.reset();
        addNewDestinationTrip(dt);
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
                            <button onClick={() => handleDelete(destination.destinationID)} type="button" className="btn btn-danger deleteButton">-</button>
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
        if(list !== null){
            return list.map((destination, index) => {
                return (
                    <tr key={index}>
                        <td>
                            <button onClick={() => handleEditDelete(destination.destinationID)} type="button" className="btn btn-danger deleteButton">-</button>
                            {destination.destinationID}
                        </td>
                        <td>
                            {destination.destination}
                        </td>
                        <td>
                            <input type="text" className="form-control inputs" id={destination.destinationID} defaultValue={destination.description} onChange={onEditChange}/>
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
                                        <input form="destinationTripForm" type="text" htmlfor="Description" className="form-control inputs descriptionInput" onChange={handleChange}/>
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
                                    <button form="destinationTripForm" type="submit" className="btn btn-primary addButton">&#43;</button>
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
                <form onSubmit={handleEditSubmit} id="destinationTripForm">
                    <table className="table table-striped" id="destinationTable">
                        <thead className="thead-dark">
                            <tr>
                                <th></th>
                                <th><h5 id="destinationHeader">DESTINATIONS</h5></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        <tr key={-3}>
                                <td>
                                    <div className="form-group">
                                        <label htmlfor="Description" className="control-label">Description</label>
                                        <input form="destinationTripForm" type="text" htmlfor="Description" className="form-control inputs descriptionInput" onChange={handleChange}/>
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
                                    <button form="destinationTripForm" type="submit" className="btn btn-primary addButton">&#43;</button>
                                </td>
                            </tr>
                            {editDestinations(updateDestinationTrips)}
                        </tbody>
                    </table>
                </form>
            </div>
        );
    }
    
}
export default DestinationsTable;