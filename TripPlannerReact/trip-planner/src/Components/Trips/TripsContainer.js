import React, {useState, useEffect} from 'react';
import TripsTable from './TripsTable';

let UserProfile = (props) => {
    const [trips, setTrips] = useState([]);

    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                //"Authorization": "Bearer " + props.token
            }
        };
        console.log(props.userID);
        fetch("https://localhost:44365/api/trips/byuser/25f160d5-d3c6-4944-b3e5-a8d6d29831c8" /*+ props.userID*/, init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("trips fetch failed")
                }
                return response.json();
            })
            .then(json => {setTrips(json)})
            .catch(console.log);
    }, [props.token]);

    let viewAddForm = (trip) => {
        console.log("Adding trip");
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

    return (   
        <div className="container d-flex align-items-center justify-content-left profile-container">
            <div className="row">
                <div className="col">
                    <TripsTable 
                        list={trips} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
            </div>
        </div>
    );
}

export default UserProfile;