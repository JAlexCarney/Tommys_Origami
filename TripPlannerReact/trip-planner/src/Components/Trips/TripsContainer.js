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

    let viewAddForm = (trip) => {
        let newState = {...state};
        newState.form = "Add";
        newState.action = (trip) => {
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
        console.log("viewing trip");
    }

    let viewDeleteForm = (trip) => {
        console.log("Deleting trip");
    }

    let viewViewForm = (trip) => {
        console.log("Viewing trip");
    }

    let exitView = () => {
        let newState = {...state};
        newState.form = "";
        newState.action = ()=>{};
        newState.trip = {};
        setState(newState);
    }

    return (   
        <div className="container-flex profile-container">
            <div className="row">
                <div className="col col-6">
                    <TripsTable 
                        list={state.list} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
                <div className="col col-6">
                    <TripsFormSelector form={state.form} agent={state.trip} action={state.action} exitAction={exitView}/>
                </div>
            </div>
        </div>
    );
}

export default UserProfile;