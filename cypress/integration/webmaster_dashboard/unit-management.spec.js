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

        const newUnitId = 'md001';

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newUnitId);

        cy.get('[role=radio]').contains('Wing').click();

        cy.intercept('POST', '/api/unit/wing').as('newWing');

        cy.contains('Submit').click();

        cy.wait('@newWing');

        cy.contains(newUnitId);
    });

    it('should create a new group given a wing', () => {
        cy.request('POST', '/api/seed/clear');

        cy.visit('/');

        cy.contains('New unit').click();

        cy.wait(300);

        const newWingId = 'md001';
        const newGroupId = 'md043';

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newWingId);

        cy.get('[role=radio]').contains('Wing').click();

        cy.intercept('POST', '/api/unit/wing').as('newWing');

        cy.contains('Submit').click();

        cy.wait('@newWing');

        cy.contains('New unit').click();

        cy.wait(300);

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newGroupId);

        cy.get('[role=radio]').contains('Group').click();
        cy.get('[role=radio]').contains(newWingId).click();

        cy.intercept('POST', '/api/unit/group').as('newGroup');

        cy.contains('Submit').click();

        cy.wait('@newGroup');

        cy.contains(newGroupId);
    });

    it('should create a new squadron given a wing and group', () => {
        cy.request('POST', '/api/seed/clear');

        cy.visit('/');

        cy.contains('New unit').click();

        cy.wait(300);

        const newWingId = 'md001';
        const newGroupId = 'md043';
        const newSquadronId = 'md089';

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newWingId);

        cy.get('[role=radio]').contains('Wing').click();

        cy.intercept('POST', '/api/unit/wing').as('newWing');

        cy.contains('Submit').click();

        cy.wait('@newWing');

        cy.contains('New unit').click();

        cy.wait(300);

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newGroupId);

        cy.get('[role=radio]').contains('Group').click();
        cy.get('[role=radio]').contains(newWingId).click();

        cy.intercept('POST', '/api/unit/group').as('newGroup');

        cy.contains('Submit').click();

        cy.wait('@newGroup');

        cy.contains('New unit').click();

        cy.wait(300);

        cy.get('#new-unit-form-id-input')
            .should('be.visible')
            .type(newSquadronId);

        cy.get('[role=radio]').contains('Squadron').click();
        cy.get('[role=radio]').contains(newWingId).click();
        cy.get('[role=radio]').contains(newGroupId).click();

        cy.intercept('POST', '/api/unit/squadron').as('newSquadron');

        cy.contains('Submit').click();

        cy.wait('@newSquadron');

        cy.contains(newGroupId);
    });
});