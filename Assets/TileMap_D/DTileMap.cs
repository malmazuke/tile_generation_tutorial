using UnityEngine;
using System.Collections.Generic;

public class DTileMap {

	protected class DRoom {
		public int left;
		public int top;
		public int width;
		public int height;
	}
	
	public enum TYPE {
		BLANK,
		FLOOR,
		WALL,
		ROCK
	}

	int size_x;
	int size_y;
	
	TYPE[,] map_data;
	
	List<DRoom> rooms;

	public DTileMap(int size_x, int size_y) {
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new TYPE[this.size_x, this.size_y];
		
		rooms = new List<DRoom>();

		for (int i = 0; i < 10; i++) {
			int r_sizeX = Random.Range(4, 8);
			int r_sizeY = Random.Range(4, 8);

			DRoom r = new DRoom();
			r.left = Random.Range(0, size_x - r_sizeX);
			r.top = Random.Range(0, size_y - r_sizeY);
			r.width = r_sizeX;
			r.height = r_sizeY;
			
			rooms.Add(r);
			
			MakeRoom(r);
		}
	}

	public TYPE GetTileAt(int x, int y) {
		return map_data [x, y];
	}

	void MakeRoom(DRoom room) {

		for (int x = 0; x < room.width; x++) {
			for (int y = 0; y < room.height; y++) {
				if (x == 0 || x == room.width - 1 || y == 0 || y == room.height - 1) {
					map_data[room.left + x, room.top + y] = TYPE.WALL;
				} else {
					map_data[room.left + x, room.top + y] = TYPE.FLOOR;
				}
			}
		}
	}
}
