import React from 'react';
import { Table } from 'react-bootstrap';

function ModuleList(props) {
    return (
        <Table>
            <thead>
                <tr>
                    <th>Имя</th>
                    <th>Дата создания</th>
                </tr>
            </thead>
            <tbody>
                {props.modules.map((module, idx) =>
                <tr key={idx}>
                    <td>{module.name}</td>
                    <td>{module.date}</td>
                </tr>
                )}
            </tbody>
        </Table>
    );
}

export default ModuleList;