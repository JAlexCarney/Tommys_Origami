import React, {useEffect, useState} from 'react';
import MostVisitedTable from './MostVisitedTable';
import MostReviewed from './MostReviewed';
import TopRatedTable from './TopRatedTable';

let ReportsContainer = (props) => {
    const [destinations, setDestinations] = useState([]);
    const [page, setPage] = useState("");

    /*
    useEffect(()=>{
        switch(page)
        {
            case "MostVisited":
                getMostVisited();
            case "MostReviewed":
                getMostReviewed();
            case "TopRated":
                getTopRated();
        }
    },[page])
    */

    let getMostVisited = () => {
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
            .then(json => setDestinations(json))
            .catch(console.log);
    }

    let getMostReviewed = () => {
        const headers = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${props.token}` 
            }
        };

        fetch("https://localhost:44365/api/reports/mostrated", headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setDestinations(json))
            .catch(console.log);
    }

    let getTopRated = () => {
        const headers = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${props.token}` 
            }
        };

        fetch("https://localhost:44365/api/reports/toprated", headers)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`Bad status: ${response.status}`);
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setDestinations(json))
            .catch(console.log);
    }

    let getTable = () => {
        switch(page)
        {
            case "MostVisited":
                return <MostVisitedTable list={destinations}/>;
            case "MostReviewed":
                return <MostReviewed list={destinations}/>;
            case "TopRated":
                return <TopRatedTable list={destinations}/>;
            default:
                return <></>;
        }
    }

    return (
        <>
            <button className="btn btn-success" onClick={()=> {setPage("MostVisited"); getMostVisited();}}>Most Visited</button>
            <button className="btn btn-success" onClick={()=> {setPage("MostReviewed"); getMostReviewed();}}>Most Reviewed</button>
            <button className="btn btn-success" onClick={()=> {setPage("TopRated"); getTopRated();}}>Top Rated</button>
            {getTable()}
        </>
    );
}

export default ReportsContainer;