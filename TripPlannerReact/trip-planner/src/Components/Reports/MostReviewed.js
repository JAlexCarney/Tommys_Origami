import React from 'react';

let MostVisitedTable = (props) => 
{
    let mapToTr = (list) =>
    {
        return list.map((destination, i) => {
            return (
                <tr key={i} className="">
                    <td className="table-data">{destination.destinationID}</td>
                    <td className="table-data">{destination.city}</td>
                    <td className="table-data">{destination.stateProvince}</td>
                    <td className="table-data">{destination.country}</td>
                    <td className="table-data">{destination.numberOfReviews}</td>
                </tr>
            );
        });
    }
    return (
        <div className="table-wrapper-scroll-y my-custom-scrollbar">
            <table className="table">
                <thead className="thead">
                    <tr>
                        <th>#</th>
                        <th>City</th>
                        <th>State/Province</th>
                        <th>Country</th>
                        <th># of Reviews</th>
                    </tr>
                </thead>
                <tbody className="trips-tbody">
                    {mapToTr(props.list)}
                </tbody>
            </table>
        </div>
    );
}
export default MostVisitedTable;