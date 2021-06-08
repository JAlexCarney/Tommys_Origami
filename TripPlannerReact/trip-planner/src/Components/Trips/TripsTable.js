import React from 'react';

let TripsTable = (props) => 
{
    let mapToTr = (list) =>
    {
        return list.map((trip, i) => {
            return (
                <tr key={i} className="trips-tr">
                    <td><button className="btn btn-secondary btn-round table-data" onClick={() => props.handleView(trip)}>{i}</button></td>
                    <td className="table-data">{trip.startDate.slice(0, 10)}</td>
                    <td><button className="btn btn-primary table-data btn-edit" onClick={() => props.handleUpdate(trip)}>Edit</button>
                    <button className="btn btn-danger table-data btn-delete" onClick={() => props.handleDelete(trip)}>Delete</button></td>
                </tr>
            );
        });
    }
    return (
        <div className="table-wrapper-scroll-y my-custom-scrollbar">
            <table className="table trips-table">
                <thead className="thead trips-thead">
                    <tr>
                        <th>#</th>
                        <th>Start Date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody className="trips-tbody">
                    <tr key={-1} className="trips-tr">
                        <td className="table-data">-</td>
                        <td className="table-data">-</td>
                        <td><button className="btn btn-primary table-data btn-add" onClick={() => props.handleAdd()}>Add Trip</button></td>
                    </tr>
                    {mapToTr(props.list)}
                </tbody>
            </table>
        </div>
    );
}
export default TripsTable;