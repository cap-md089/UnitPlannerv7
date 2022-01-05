// unit-management.spec.js: Integration tests for creating and managing units
//
// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

describe('unit management', () => {
    it('should create a new unit', () => {
        cy.request('POST', '/api/seed/clear');

        cy.visit('/');

        cy.contains('New unit').click();

        cy.wait(300);

        const newUnitId = 'md089';

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newUnitId);

        cy.contains('Submit').click();

        cy.visit('/');

        cy.contains(newUnitId);
    });
});