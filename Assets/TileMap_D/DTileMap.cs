using UnityEngine;
using System.Collections.Generic;

public class DTileMap {

	protected class DRoom {
		public int left;
		public int top;
		public int width;
		public int height;
		
		public int right {
			get { return this.left + this.width - 1; }
		}
		
		public int bottom {
			get { return this.top + this.height - 1; }
		}
		
		public bool CollidesWith(DRoom other) {
			if (this.left   > other.right  - 1) return false;
			if (this.top    > other.bottom - 1) return false;
			if (this.right  < other.left   + 1) return false;
			if (this.bottom < other.top    + 1) return false;
			
			return true;
		}
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
		
		for (int x = 0; x < this.size_x; x++) {
			for (int y = 0; y < this.size_y; y++) {
				map_data[x,y] = TYPE.ROCK;
			}
		}
		
		rooms = new List<DRoom>();

		for (int i = 0; i < 20; i++) {
			int r_sizeX = Random.Range(4, 8);
			int r_sizeY = Random.Range(4, 8);

			DRoom r = new DRoom();
			r.left = Random.Range(0, size_x - r_sizeX);
			r.top = Random.Range(0, size_y - r_sizeY);
			r.width = r_sizeX;
			r.height = r_sizeY;
			
			if (!RoomCollides(r)) {
				rooms.Add(r);
			}
		}
		
		foreach(DRoom r in rooms) {
			MakeRoom(r);
		}
	}

	bool RoomCollides(DRoom r) {
		foreach(DRoom r2 in rooms) {
			if (r.CollidesWith(r2)){
				return true;
			}
		}
		
		return false;
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
