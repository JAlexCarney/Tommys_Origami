import React, {useState, useEffect} from 'react';

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

    console.log(trips);

    return (
        <div className="row">
            Hello
        </div>
    );
}

export default UserProfile;