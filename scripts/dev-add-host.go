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

package main

import (
	"log"
	"os"
	"strings"
)

func main() {
	if len(os.Args) < 2 {
		log.Panic("No argument provided")
	}

	host_text, err := os.ReadFile("/etc/hosts")
	if err != nil {
		log.Panic(err)
	}

	if strings.Contains(string(host_text), os.Args[1]) {
		println("dev-add-host: host already added")
		os.Exit(0)
	}

	hosts, err := os.OpenFile("/etc/hosts", os.O_APPEND|os.O_WRONLY, 0644)
	if err != nil {
		log.Panic(err)
	}

	_, err = hosts.Write([]byte(os.Args[1]))
	if err != nil {
		log.Panic(err)
	}
	_, err = hosts.Write([]byte("\n"))
	if err != nil {
		log.Panic(err)
	}

	hosts.Close()

	os.Exit(0)
}
