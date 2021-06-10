import React, {useState, useEffect} from 'react';

let DestinationTripView = (props) => {
    const [destinationTrips, setDestinationTrips] = useState([]);

    let setDestinationNames = (userDestinations) => {
        const headers = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${props.token}` 
            }
        };
        let url = "https://localhost:44365/api/reports/destinationtripswithcity";
        fetch(url, headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then((json) => {
                let destinationNames = [];
                for(let i = 0; i < json.length; i++)
                {
                    for(let j = 0; j < userDestinations.length; j++)
                    {
                        if(json[i].destinationID === userDestinations[j].destinationID
                            && json[i].tripID === userDestinations[j].tripID){
                            let destinationName = {...json[i]};
                            destinationName.destination = json[i].cityCountry;
                            destinationNames.push(destinationName);
                        }
                    }
                }
                setDestinationTrips(destinationNames);
            })
            .catch(console.log);
        
    }
    
    useEffect(() => {
        if(props.tripID !== undefined)
        {
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
                .then((json) => {
                    setDestinationNames(json);
                })
                .catch(console.log);
        }
    }, [props.tripID]);
    
    let DestinationToTr = (list) =>
    {
        if(list !== null){
            return list.map((destination, index) => {
                return (
                    <tr key={index}>
                        <td className="table-data">
                            {destination.destination}
                        </td>
                        <td className="table-data">
                            {destination.description}
                        </td>
                    </tr>
                );
            });
        }
    }

    return (
        <div>
            <h4 className="text-center">DESTINATIONS</h4>
            <table className="table table-striped" id="destinationTable">
                <thead>
                    <tr>
                        <th className="table-data">Destination</th>
                        <th className="table-data">Description</th>
                    </tr>
                </thead>
                <tbody>
                    {DestinationToTr(destinationTrips)}
                </tbody>
            </table>
        </div>
    );
}

export default DestinationTripView;