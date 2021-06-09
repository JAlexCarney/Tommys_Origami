import React, {useState, useEffect} from 'react';
import TripsTable from './TripsTable';
import TripsFormSelector from './TripsFormSelector';

let UserProfile = (props) => {
    const [state, setState] = useState({list:[], form:"", action:()=>{}, trip:{}});

    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + props.token
            }
        };
        fetch("https://localhost:44365/api/trips/byuser/" + props.userID, init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("trips fetch failed")
                }
                return response.json();
            })
            .then(json => {
                let newState = {list:[], form:"", action:()=>{}, trip:{}};
                newState.list = json;
                setState(newState);
            })
            .catch(console.log);
    }, [props.token, props.userID]);

    let addDestinationTrips = (tripID, list) => {
        let destinationTrips = list.map((dt) => {
            return (
                {
                    tripID: tripID,
                    destinationID: dt.destinationID,
                    description: dt.description
                }
            )
        })
        for(let i = 0; i < destinationTrips.length; i++){
            const init = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(destinationTrips[i])
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

        }

    let viewAddForm = (trip) => {
        let newState = {...state};
        newState.form = "Add";
        newState.action = (trip, list) => {
            let tripWithUser = {...trip};
            tripWithUser.userID = props.userID;
            console.log(tripWithUser);
            const init = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(tripWithUser)
              };
          
            fetch("https://localhost:44365/api/trips", init)
                .then(response => {
                    if (response.status !== 201) {
                        return Promise.reject("response is not 200 OK");
                    }
                    return response.json();
                })
                .then((json) => {
                    addDestinationTrips(json.tripID, list);
                    let newState = {...state};
                    newState.list.unshift(json);
                    newState.form = "";
                    newState.action = ()=>{};
                    newState.trip = {};
                    setState(newState);
                })
                .catch(console.log);
        };
        newState.trip = trip;
        setState(newState);
    }

    let viewUpdateForm = (trip) => {
        let newState = {...state};
        newState.form = "Edit";
        newState.action = (trip) => {
            let tripWithUser = {...trip};
            tripWithUser.userID = props.userID;
            console.log(tripWithUser);
            const init = {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(tripWithUser)
              };
          
            fetch("https://localhost:44365/api/trips", init)
                .then(response => {
                    if (response.status !== 200) {
                        console.log(response.status);
                        return Promise.reject("response is not 200 OK");
                    }
                    // return response.json();
                })
                .then(() => {
                    let newState = {...state};
                    let i = state.list.findIndex(t => t.tripID === trip.tripID)
                    newState.list = [...state.list];
                    newState.list[i] = trip;
                    newState.form = "";
                    newState.action = ()=>{};
                    newState.trip = {};
                    setState(newState);
                })
                .catch(console.log);
        };
        newState.trip = trip;
        setState(newState);
    }

    let viewDeleteForm = (trip) => {
        let newState = {...state};
        newState.form = "Delete";
        newState.action = (trip) => {
            console.log("Attempting to delete");
            const init = {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                }
              };
          
            fetch(`https://localhost:44365/api/trips/${trip.tripID}`, init)
                .then(response => {
                    if (response.status === 200) {
                        let newState = {...state};
                        newState.list = state.list.filter(t => t.tripID  !== trip.tripID);
                        newState.form = "";
                        newState.action = ()=>{};
                        newState.trip = {};
                        setState(newState);
                    }
                    return Promise.reject("response is not 200 OK");
                })
                .catch(console.log);
        };
        newState.trip = trip;
        setState(newState);
    }

    let viewViewForm = (trip) => {
        let newState = {...state};
        newState.form = "View";
        newState.action = ()=>{};
        newState.trip = trip;
        setState(newState);
    }

    let exitView = () => {
        let newState = {...state};
        newState.form = "";
        newState.action = ()=>{};
        newState.trip = {};
        setState(newState);
    }

    let tableSize="col col-6 trips-table-col";
    if(state.form === "")
    {
        tableSize="col col-12 trips-table-col";
    }

    return (   
        <div className="container-flex profile-container">
            <div className="row">
                <div className={tableSize}>
                    <TripsTable 
                        list={state.list}
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
                <div className="col col-6">
                    <TripsFormSelector form={state.form} trip={state.trip} action={state.action} exitAction={exitView}/>
                </div>
            </div>
        </div>
    );
}

export default UserProfile;