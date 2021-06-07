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
            .then(json => {setTrips(json)})
            .catch(console.log);
    }, [props.token, props.userID]);

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
        <div className="container-flex profile-container">
            <div className="row">
                <div className="col col-6">
                    <TripsTable 
                        list={trips} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
                <div className="col col-6">
                    
                </div>
            </div>
        </div>
    );
}

export default UserProfile;